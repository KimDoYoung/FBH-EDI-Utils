#!/bin/bash
#
# Edi Programs Deploy
#
TIME=$(date '+%Y%m%d_%H%M%S')
YMD=$(date '+%Y-%m-%d')
BASE_SRC_DIR="$HOME/source/repos/FBH-EDI-Utils"
BASE_DES_DIR="$HOME/Documents/kdy/fbh-edi-programs/$YMD"
# ymd base의 des directory 삭제
rm -rf ${BASE_DES_DIR}
mkdir -p ${BASE_DES_DIR}

# cp 
declare -a programs=( "EdiDiff" "EdiRename" "EdiListMaker" "EdiViewer" "EdiDbUploader" )
for program in "${programs[@]}"; do
   AsmFile="$BASE_SRC_DIR/$program/Properties/AssemblyInfo.cs"
   AsmLine=$(awk '/AssemblyFileVersion/{print $0}' $AsmFile)
   VER=$(echo $AsmLine | grep -Eo "[0-9]{1,2}.[0-9]{1,2}.[0-9]{1,2}.[0-9]{1,2}")
   #echo $AsmLine
   #echo $VER
    cp -R "${BASE_SRC_DIR}/${program}/bin/Release" "${BASE_DES_DIR}/${program}-$VER"
done

echo "DONE"
