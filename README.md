# KadaikyokuBot
## Overview
某スライド擦ってヘドバンするリズムゲームの課題曲を生成するDiscord Botです。

## Features

### Commands
+ `!kadaikyoku` とチャットすることで、WORLD'S ENDを含む全曲から課題曲を生成します。
+ `!kadaikyoku <minLv> <maxLv>` とチャットすることで、`minLv` ～ `maxLv` までのレベルの範囲で課題曲を生成できます。
  - 引数は譜面定数で指定してください。

## TODO
+ `!search` で楽曲検索を利用できるようにする。
+ `!recommend` で自身の適性レート帯の楽曲かつ、ランクSSSを獲得していない楽曲から課題曲を生成できるようにする。
+ `!kadaikyoku` 実行する際に引数に、ジャンルと難易度でも絞り込みできるようにする。

## Special Thanks

### Chunirec API
+ https://chunirec.net/
