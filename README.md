# procon33
## それぞれのファイルについて
## procon33_gui
C#にて書かれたプロコン用のGUIツール
## scripts
### 音声に関するスクリプトについて
### search_part.py
音声の相関を調べるスクリプト
### テスト用のスクリプトについて
### makequal.py
入力を受けた音声を重ねるスクリプト
### setup.py
音源データがなければダウンロードをして、num変数の数字の数分の音声データをmakequal.py使って合成するスクリプト
### test_case.py
search_part.pyを実行して結果をall_result.pyに入れるスクリプト
## 問題を解くためのスクリプト
###isolation
音声を分離するスクリプト
### collect.py
all_resultの結果を集計するスクリプト
