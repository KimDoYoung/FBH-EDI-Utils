# EdiDiff

## 개요

1. 850을 중심으로 945에서 품목을 찾아서 갯수가 맞는지 체크한다.
2. 입력이 되는 850, 945 리스트 엑셀파일은 EdiListMaker(EdiUtils)에서 만들어진 파일이어야한다.
3. 체크한 항목들을 보여주는 excel을 만든다.
4. 945에서는 po + item no(retail's item no)로 찾는다.
5. 찾은 결과는 1) 발견못함 2)일치 3) 불일치 3개로 보여준다.


## 특징

1. 딸기 아이콘을 사용



## 개발시 필요한 패키지

- NuGet Package Manager에서  **Microsoft.Office.Interop.Excel** 찾아서 설치해야한다.
- [엑셀에 필요한 라이브러리](https://www.freecodespot.com/blog/csharp-import-excel/)


