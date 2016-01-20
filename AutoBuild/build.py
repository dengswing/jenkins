#!/bin/sh

#参数判断  
if [ $# != 1 ];then  
    echo "需要一个参数。 参数是游戏包的名子"  
    exit     
fi  


#游戏程序路径#
PROJECT_PATH=/Users/shinezone/.jenkins/jobs/Ios/workspace/IOS

#IOS打包脚本路径#
BUILD_IOS_PATH=/Users/shinezone/.jenkins/jobs/Ios/workspace/AutoBuild/buildios.sh


#开始生成ipa#
$BUILD_IOS_PATH $PROJECT_PATH $1

echo "ipa生成完毕"