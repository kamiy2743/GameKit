日本語で回答して

# Repository Guidelines

## プロジェクト構成とモジュール
本リポジトリは Unity の GameKit を構成する複数モジュールを管理し、主要コードは `Assets/GameKit/<Module>/Scripts` 以下にまとめています。`Application`、`Camera`、`CharacterController` といったサブフォルダ毎に `*.asmdef` を分割し、依存を明文化しています。
共有リソースは `Assets/Resources`、設定アセットは `Assets/Settings`、プレハブやシーンは `Assets/Scenes` に配置してください。バッチビルドなどで生成された成果物は `Build/` にまとめ、IDE での編集時はルートの `GameKit.sln` を開くと各モジュールを横断できます。

## ビルド・テスト・開発コマンド
Unity エディタ上での確認に加え、コマンドラインでは `Unity.exe -batchmode -quit -projectPath . -buildTarget StandaloneWindows64 -buildProfile "Assets/Settings/Build Profiles/Debug.asset"` を使い Debug ビルドを生成します。C# ソースのコンパイルチェックだけを行う場合は `dotnet build GameKit.sln` を利用し、`Library/` を消さずに差分検証できます。
自動テストは Unity Test Runner を利用し、EditMode は `Unity.exe -batchmode -projectPath . -runTests -testPlatform EditMode -testResults Logs/EditMode.xml`、PlayMode は `-testPlatform PlayMode` に切り替えて実行します。ログ出力は `Logs/` にまとまるため CI ではアーティファクトとして収集してください。

## コーディング規約と命名
C# コードは 4 スペースインデント、UTF-8、BOM なしを原則とします。クラスと構造体は `PascalCase`、フィールドは `_camelCase`、定数は `PascalCase` を用い、Unity イベントメソッドは既定シグネチャに従います。
名前空間は `GameKit.<Module>` を基準に拡張し、Editor 専用コードは `Editor` 名前空間とフォルダに隔離してください。Rider もしくは VS のコード整形 (`Ctrl+Alt+L` / `Ctrl+K,D`) をコミット前に実行します。

## テスト指針
各モジュールに `Tests` フォルダを追加し、`<Feature>Tests.cs` の命名で EditMode テストを作成します。新規 API は少なくとも正常系とエラー系をカバーし、Mock が必要な場合は `DisposableExtension` を活用します。
PlayMode テストはサンプルシーンを複製し、`Assets/Scenes/Tests` に配置したシーンを使用してください。テスト実行後は `Logs/EditMode.xml` と `Logs/PlayMode.xml` を PR に添付し、失敗時のリグレッションを再現できるようにします。

## コミットとプルリクエスト
コミットメッセージは Git 履歴に倣い、先頭で変更目的を短い日本語フレーズでまとめ、必要に応じて関連チケット番号を括弧書きします。複数の論点にまたがる場合はコミットを分割し、動作確認が取れた単位で積み上げてください。
プルリクエストでは概要、検証手順、影響範囲、スクリーンショット／動画（UI 変更時）を箇条書きで提示します。新規設定値を追加した際は `Assets/Settings` への反映方法を説明し、ビルド確認結果も併記してください。

## 設定と運用 Tips
`Packages/manifest.json` に直接依存を追加する前に、モジュール内の `package.json` で管理可能か確認します。`ProjectSettings/` は Unity により自動更新されるため、変更意図があるファイルのみ PR に含めてください。
CI やローカル自動化で一時ファイルが増えた場合は `.gitignore` に追記し、`Temp/` や `Library/` を誤ってコミットしないよう注意します。
