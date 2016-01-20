#!/bin/sh

#参数判断  
if [ $# != 2 ];then
    echo "需要2个参数。 参数是游戏包的名子,脚本路径"
    exit     
fi  
    
Work_Path=$2

#游戏程序路径#
PROJECT_PATH=$Work_Path/IOS

#IOS打包脚本路径
BUILD_IOS_PATH=$Work_Path/AutoBuild/buildios.sh


#开始生成ipa#
$BUILD_IOS_PATH $PROJECT_PATH $1
    
echo "ipa生成完毕"