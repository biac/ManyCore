# ManyCore

[Ｃ#による マルチコアのための非同期／並列処理プログラミング](http://gihyo.jp/book/2013/978-4-7741-5828-0) (2013年刊)

- [**Win10VS2017**/](./Win10VS2017): サンプルコードを Windows 10 向けに移植したもの。 ビルドするには Visual Studio 2017 が必要です。  
ロジックの部分は、書籍に掲載したコード (Windows 8.x 向け) とほとんど同じです。  
UI 部分は、フレームワークが UWP に変わったことに加えて、描画を Win2D (DirectX) で行うようにしたため、かなりの相違があります。 UI からロジックにアクセスする部分はほぼ同じです。
- [Win10VS2017/**C#MultiCore_Part4_Win10VS2017.txt**](./Win10VS2017/C%23MultiCore_Part4_Win10VS2017.txt ): お読みください。
- [Win10VS2017/**Part04Chapter02**/](./Win10VS2017/Part04Chapter02): PART 4 Chapter 2　最初の実装
- [Win10VS2017/**Part04Chapter03**/](./Win10VS2017/Part04Chapter03): PART 4 Chapter 3　ロジックと画面を非同期化する
- [Win10VS2017/**Part04Chapter04**/](./Win10VS2017/Part04Chapter04): PART 4 Chapter 4　ロジックを並列化する
- [Win10VS2017/**Part04Chapter05**/](./Win10VS2017/Part04Chapter05): PART 4 Chapter 5　ロジックを改良する
  
なお、UWP アプリは、リリースビルドではネイティブコンパイルされます。リリースビルドには時間が掛かりますが、それによる高速化の効果も確かめてみてください。
  
● **PART 4 Chapter 2** (デバッグビルド)  
　※ここでのサイクルタイムは、計算1回 + 描画1回に掛かった時間  
![スクリーンキャプチャー](./images/Win10VS2017_Part04Chapter05_02.png)
  
**⇩** 非同期化 + 並列化 + ロジックチューニング + ネイティブコンパイル  
  
● **PART 4 Chapter 5** (リリースビルド)  
　※ここでのサイクルタイムは、計算1回に掛かった時間 (描画は非同期に行われる)  
![スクリーンキャプチャー](./images/Win10VS2017_Part04Chapter05_01.png)
