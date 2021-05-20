# learning-effective-csharp
Effective C#勉強用のサンプルコード置き場

## 使い方
 - 1つの.slnファイルにプロジェクトをまとめています
 - 試したいサンプルコードのプロジェクトファイルを右クリックしてスタートアッププロジェクトに設定してから実行してください

### ch9-sample-struct-copy
Effective C# の項目9に記載されている注意が必要なケースのサンプルコード（ほぼ写経）


### ch9-sample-boxing_unboxing
Effective C# の項目9でボックス化・ボックス化解除でパフォーマンスが変わるとあったのでどれ位変わるのかのお試しコード。
簡単な処理をループでたくさん実行したら差がでるのでは？という観点で作成。

自環境で100万回ループで
1. ボックス化、ボックス化解除
2. ボックス化なし
で、平均3.4msecの差がでた。

計測用にTimerを使っていたり、そもそももっと良い計測処理があるのでは？と思うが一旦これくらいで。
