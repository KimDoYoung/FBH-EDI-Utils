#!/bin/bash
#
#C:\Users\deHong\source\repos\FBH-EDI-Utils
#
TIME=$(date '+%Y%m%d_%H%M%S')
YMD=$(date '+%Y-%m-%d')
BASE_SRC_DIR=/C/Users/deHong/source/repos/FBH-EDI-Utils
BASE_DES_DIR=$HOME/Documents/kdy/fbh-edi-programs/$YMD
# ymd base의 des directory 삭제
rm -rf ${BASE_DES_DIR}
mkdir -p ${BASE_DES_DIR}

# cp 
declare -a programs=( "EdiDiff" "EdiRename" "EdiListMaker" "EdiViewer" "EdiDbUploader" )
for program in "${programs[@]}"; do
    cp -R "${BASE_SRC_DIR}/${program}/bin/Release" "${BASE_DES_DIR}/${program}"
done

echo "DONE"
