#!/bin/sh

#参数判断  
if [ $# != 1 ];then  
    echo "需要一个参数。 参数是游戏包的名子"  
    exit     
fi  

Work_Path=$1

#游戏程序路径#
PROJECT_PATH=$Work_Path/IOS

#IOS打包脚本路径
BUILD_IOS_PATH=$Work_Path/AutoBuild/buildios.sh


#开始生成ipa#
$BUILD_IOS_PATH $PROJECT_PATH $Work_Path
    
echo "ipa生成完毕"