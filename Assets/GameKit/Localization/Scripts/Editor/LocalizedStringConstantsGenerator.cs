using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

namespace GameKit.Localization.Editor
{
    /// <summary>
    /// Detects StringTableCollection updates and enumerates all StringReferences.
    /// Automatically regenerates <see cref="LocalizedString">LocalizedString</see> constants.
    /// </summary>
    public static class LocalizedStringConstantsGenerator
    {
        static readonly StringBuilder SourceBuilder = new(4096);
        static readonly UTF8Encoding Utf8EncodingWithoutBom = new(false);

        sealed class StringTableCollectionPostprocessor : AssetPostprocessor
        {
            static void OnPostprocessAllAssets(
                string[] importedAssets,
                string[] deletedAssets,
                string[] movedAssets,
                string[] movedFromAssetPaths)
            {
                if (importedAssets.Length == 0)
                {
                    return;
                }

                var processedCollections = new HashSet<string>();
                foreach (var assetPath in importedAssets)
                {
                    if (!TryGetCollectionFromAssetPath(assetPath, out var collection))
                    {
                        continue;
                    }

                    if (!processedCollections.Add(GetCollectionKey(collection)))
                    {
                        continue;
                    }

                    HandleCollectionUpdated(collection);
                }
            }
        }

        static string GetCollectionKey(StringTableCollection collection)
        {
            var sharedData = collection.SharedData;
            if (sharedData != null && sharedData.TableCollectionNameGuid != Guid.Empty)
            {
                return sharedData.TableCollectionNameGuid.ToString();
            }

            return collection.TableCollectionName;
        }

        static bool TryGetCollectionFromAssetPath(string assetPath, out StringTableCollection collection)
        {
            collection = AssetDatabase.LoadAssetAtPath<StringTableCollection>(assetPath);
            if (collection != null)
            {
                return true;
            }

            var sharedData = AssetDatabase.LoadAssetAtPath<SharedTableData>(assetPath);
            if (TryResolveCollection(sharedData, out collection))
            {
                return true;
            }

            var stringTable = AssetDatabase.LoadAssetAtPath<StringTable>(assetPath);
            return TryResolveCollection(stringTable, out collection);
        }

        static bool TryResolveCollection(StringTable table, out StringTableCollection collection)
        {
            collection = null;
            if (table == null)
            {
                return false;
            }

            TableReference tableReference = table.TableCollectionName;
            if (TryResolveCollection(tableReference, out collection))
            {
                return true;
            }

            return TryResolveCollection(table.SharedData, out collection);
        }

        static bool TryResolveCollection(SharedTableData sharedData, out StringTableCollection collection)
        {
            collection = null;
            if (sharedData == null)
            {
                return false;
            }

            TableReference tableReference = sharedData.TableCollectionNameGuid;
            if (tableReference.ReferenceType == TableReference.Type.Empty && !string.IsNullOrEmpty(sharedData.TableCollectionName))
            {
                tableReference = sharedData.TableCollectionName;
            }

            return TryResolveCollection(tableReference, out collection);
        }

        static bool TryResolveCollection(TableReference tableReference, out StringTableCollection collection)
        {
            collection = null;
            if (tableReference.ReferenceType == TableReference.Type.Empty)
            {
                return false;
            }

            collection = LocalizationEditorSettings.GetStringTableCollection(tableReference);
            return collection != null;
        }

        static void HandleCollectionUpdated(StringTableCollection collection)
        {
            GenerateConstantsAsset();
        }

        static void GenerateConstantsAsset()
        {
            var settings = LocalizedStringConstantsGeneratorSetting.Instance;
            if (settings == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(settings.NamespaceName) || string.IsNullOrWhiteSpace(settings.OutputPath))
            {
                return;
            }

            var namespaceName = settings.NamespaceName.Trim();
            var outputDirectory = settings.OutputPath.Trim().TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            if (string.IsNullOrEmpty(outputDirectory))
            {
                return;
            }

            var outputPath = Path.Combine(outputDirectory, "LocalizedStringConstants.cs").Replace('\\', '/');

            var collections = LocalizationEditorSettings.GetStringTableCollections();
            var groupTables = new Dictionary<string, List<TableDeclaration>>(StringComparer.Ordinal);
            var groupClassNames = new Dictionary<string, string>(StringComparer.Ordinal);

            foreach (var collection in collections)
            {
                var references = EnumerateStringReferences(collection);
                if (references.Count == 0)
                {
                    continue;
                }

                var fullTableName = collection.TableCollectionName ?? string.Empty;
                var separatorIndex = fullTableName.IndexOf('.');
                var groupKey = separatorIndex >= 0 ? fullTableName.Substring(0, separatorIndex) : string.Empty;
                var tableNamePart = separatorIndex >= 0 && separatorIndex + 1 < fullTableName.Length ?
                    fullTableName.Substring(separatorIndex + 1) :
                    fullTableName;
                if (string.IsNullOrEmpty(tableNamePart))
                {
                    tableNamePart = fullTableName;
                }
                var className = ToIdentifier(tableNamePart, "Table");
                var entryDeclarations = BuildEntryDeclarations(references);
                if (entryDeclarations.Count == 0)
                {
                    continue;
                }

                if (!groupTables.TryGetValue(groupKey, out var tables))
                {
                    tables = new List<TableDeclaration>();
                    groupTables[groupKey] = tables;

                    var groupNameForIdentifier = string.IsNullOrEmpty(groupKey) ? "Global" : groupKey;
                    groupClassNames[groupKey] = ToIdentifier(groupNameForIdentifier, "Group");
                }

                tables.Add(new TableDeclaration(className, collection.TableCollectionName, entryDeclarations));
            }

            var groupDeclarations = new List<GroupDeclaration>(groupTables.Count);
            foreach (var (key, tables) in groupTables)
            {
                tables.Sort((a, b) => string.CompareOrdinal(a.TableName, b.TableName));
                groupDeclarations.Add(new GroupDeclaration(groupClassNames[key], key, tables.ToArray()));
            }

            groupDeclarations.Sort((a, b) => string.CompareOrdinal(a.GroupKey, b.GroupKey));

            WriteConstantsFile(groupDeclarations, namespaceName, outputPath);
            AssetDatabase.ImportAsset(outputPath);
        }
        static IReadOnlyList<EntryDeclaration> BuildEntryDeclarations(IReadOnlyList<StringReferenceInfo> references)
        {
            var sortedReferences = new List<StringReferenceInfo>(references);
            sortedReferences.Sort((a, b) => string.CompareOrdinal(a.EntryKey, b.EntryKey));

            var declarations = new List<EntryDeclaration>(sortedReferences.Count);
            var usedNames = new HashSet<string>(StringComparer.Ordinal);
            foreach (var reference in sortedReferences)
            {
                var constantName = MakeUniqueIdentifier(ToIdentifier(reference.EntryKey, "Entry"), usedNames);
                declarations.Add(new EntryDeclaration(constantName, reference.EntryKey));
            }

            return declarations;
        }
        static void WriteConstantsFile(IReadOnlyList<GroupDeclaration> groups, string namespaceName, string assetPath)
        {
            SourceBuilder.Clear();
            SourceBuilder.AppendLine("// <auto-generated />");
            SourceBuilder.AppendLine("// Generated by LocalizedStringConstantsGenerator. Do not edit manually.");
            SourceBuilder.AppendLine();
            SourceBuilder.AppendLine("using GameKit.Localization;");
            SourceBuilder.AppendLine();
            SourceBuilder.Append("namespace ")
                .Append(namespaceName)
                .AppendLine();
            SourceBuilder.AppendLine("{");
            SourceBuilder.AppendLine("    public static class LocalizedStringConstants");
            SourceBuilder.AppendLine("    {");

            if (groups.Count > 0)
            {
                for (var groupIndex = 0; groupIndex < groups.Count; groupIndex++)
                {
                    var group = groups[groupIndex];
                    SourceBuilder.Append("        public static class ")
                        .Append(group.ClassName)
                        .AppendLine()
                        .AppendLine("        {");

                    if (group.Tables.Count > 0)
                    {
                        for (var tableIndex = 0; tableIndex < group.Tables.Count; tableIndex++)
                        {
                            var table = group.Tables[tableIndex];
                            SourceBuilder.Append("            public static class ")
                                .Append(table.ClassName)
                                .AppendLine()
                                .AppendLine("            {");

                            foreach (var entry in table.Entries)
                            {
                                SourceBuilder.Append("                public static readonly LocalizedString ")
                                    .Append(entry.ConstantName)
                                    .Append(" = new(nameof(")
                                    .Append(group.ClassName)
                                    .Append("), nameof(")
                                    .Append(table.ClassName)
                                    .Append("), nameof(")
                                    .Append(entry.ConstantName)
                                    .Append(")")
                                    .AppendLine(");");
                            }

                            SourceBuilder.AppendLine("            }");

                            if (tableIndex < group.Tables.Count - 1)
                            {
                                SourceBuilder.AppendLine();
                            }
                        }
                    }

                    SourceBuilder.AppendLine("        }");

                    if (groupIndex < groups.Count - 1)
                    {
                        SourceBuilder.AppendLine();
                    }
                }
            }

            SourceBuilder.AppendLine("    }");
            SourceBuilder.AppendLine("}");

            var fullPath = Path.GetFullPath(assetPath);
            var directory = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(fullPath, SourceBuilder.ToString(), Utf8EncodingWithoutBom);
        }

        static string ToIdentifier(string source, string fallback)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return fallback;
            }

            var builder = new StringBuilder(source.Length);
            var capitalize = true;
            foreach (var ch in source)
            {
                if (char.IsLetterOrDigit(ch) || ch == '_')
                {
                    builder.Append(capitalize ? char.ToUpperInvariant(ch) : ch);
                    capitalize = false;
                }
                else
                {
                    capitalize = true;
                }
            }

            var identifier = builder.ToString();
            if (string.IsNullOrEmpty(identifier))
            {
                identifier = fallback;
            }

            if (!char.IsLetter(identifier[0]) && identifier[0] != '_')
            {
                identifier = "_" + identifier;
            }

            return identifier;
        }

        static string MakeUniqueIdentifier(string candidate, HashSet<string> used)
        {
            var unique = candidate;
            var index = 1;
            while (!used.Add(unique))
            {
                unique = candidate + index;
                index++;
            }

            return unique;
        }

        static string EscapeString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return value.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        static IReadOnlyList<StringReferenceInfo> EnumerateStringReferences(StringTableCollection collection)
        {
            if (collection == null)
            {
                return Array.Empty<StringReferenceInfo>();
            }

            var sharedData = collection.SharedData;
            if (sharedData == null || sharedData.Entries.Count == 0)
            {
                return Array.Empty<StringReferenceInfo>();
            }

            var tableReference = collection.TableCollectionNameReference;
            var references = new List<StringReferenceInfo>(sharedData.Entries.Count);
            foreach (var entry in sharedData.Entries)
            {
                if (entry == null || string.IsNullOrWhiteSpace(entry.Key))
                {
                    continue;
                }

                TableEntryReference entryReference = entry.Id;
                references.Add(new StringReferenceInfo(tableReference, entryReference, entry.Key));
            }

            return references;
        }

        readonly struct StringReferenceInfo
        {
            public StringReferenceInfo(TableReference table, TableEntryReference entry, string entryKey)
            {
                Table = table;
                Entry = entry;
                EntryKey = entryKey;
            }

            public TableReference Table { get; }
            public TableEntryReference Entry { get; }
            public string EntryKey { get; }

            public override string ToString()
            {
                return $"{Table.ToString()}.{EntryKey} ({Entry.ToString()})";
            }
        }

        readonly struct GroupDeclaration
        {
            public GroupDeclaration(string className, string groupKey, IReadOnlyList<TableDeclaration> tables)
            {
                ClassName = className;
                GroupKey = groupKey;
                Tables = tables;
            }

            public string ClassName { get; }
            public string GroupKey { get; }
            public IReadOnlyList<TableDeclaration> Tables { get; }
        }
        readonly struct TableDeclaration
        {
            public TableDeclaration(string className, string tableName, IReadOnlyList<EntryDeclaration> entries)
            {
                ClassName = className;
                TableName = tableName;
                Entries = entries;
            }

            public string ClassName { get; }
            public string TableName { get; }
            public IReadOnlyList<EntryDeclaration> Entries { get; }
        }

        readonly struct EntryDeclaration
        {
            public EntryDeclaration(string constantName, string entryKey)
            {
                ConstantName = constantName;
                EntryKey = entryKey;
            }

            public string ConstantName { get; }
            public string EntryKey { get; }
        }
    }
}




















