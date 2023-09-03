# postgresql 명령어들 - FBH


## 데이터베이스 생성
데이터베이스를 만들 때 데이터베이스에서 사용하는 
**문자 셋 (ENCODING)** 및 
**문자열 정렬 순서(LC_COLLATE)**와 **문자 분류(LC_CTYPE)**는 명시적으로 지정하지 않은 경우에는 기본값이 사용되지만, 이 설정들은 한 번 데이터베이스를 만들면 변경할 수 없으므로 주의해야 한다.

```
login postgresql
CREATE DATABASE fbhdb OWNER  kdy987;
grant all privileges on database fbhdb to kdy987;

login kdy987
CREATE SCHEMA s1;
SELECT * from information_schema.schemata;
```

## Role

-  데이터베이스 권한들을 특정 이름으로 모아 만드는 것을 role 이라한다.
