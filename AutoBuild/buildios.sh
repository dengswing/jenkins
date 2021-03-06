# #!/bin/bash  

echo $1
echo $2
echo $3

#参数判断
if [ $# != 3 ];then
    echo "Params error!"  
    echo "Need two params: 1.path of project 2.name of ipa file"  
    exit  
elif [ ! -d $1 ];then  
    echo "The first param is not a dictionary."  


fi  
#工程路径  
project_path=$1

#IPA名称  
ipa_name=$2

export_path=$3

#build文件夹路径  
build_path=${project_path}/build

if [ -d "$export_path"]; then
rmdir "$export_path"
mkdir "$export_path"
else
mkdir "$export_path"
fi

#清理#
xcodebuild  clean

#编译工程  
cd $project_path  
xcodebuild || exit  

#打包 下面代码我是新加的#  
xcrun -sdk iphoneos PackageApplication -v ${build_path}/Release-iphoneos/*.app -o ${export_path}/${ipa_name}.ipa
