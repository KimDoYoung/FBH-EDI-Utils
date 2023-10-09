# EdiDbUploader

## 개요

1. EDI 문서들을 읽어서 
2. 종류를 판별하고
3. DB에 업로드한다.
4. 이미 있으면 update하지 않는다. 그냥 skip

> 순전히 엑셀EDI문서로부터 DB를 적재하기 위해서만 사용된다.

## PostgreSQL관련 패키지 설치

Package Manager는 콘솔창과 비슷한 방식으로 명령을 실행할 수 있는데, .NET을 위한 PostgreSQL 라이브러리를 설치하기 위해 아래의 명령을 입력합니다.

현재 시점에서는 4.0.4가 최신버전이지만 이를 확인하기 위해 아래의 URL로 접속하기 바랍니다.

- https://www.nuget.org/packages/Npgsql/ 최신버젼확인
- Install-Package Npgsql -Version 7.0.4 

## rename기능 넣기.

- 인스피언에서 다운받은 파일의 이름은 UUID로 되어 있는데, 그것의 이름을 여기서 바꾼다.
- 먼저 DB에 올리고 이름을 변경
- 이름바꾼것초기폴더 문서/edi

## history

- 2023-09-18 : 1.0.0.0 배포
- 2023-10-05 : 1.3.0.0 Delivery Appointments와 Walmart810Payment 2개의 테이블
- 2023-10-09 : 1.4.0.0 810에 woy을 넣음.


