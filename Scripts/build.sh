#!/usr/bin/env bash

# Example build script for Unity3D project. See the entire example: https://github.com/JonathanPorta/ci-build

# Change this the name of your project. This will be the name of the final executables as well.
project="zappy_gfx"

if [[ ${TRAVIS_OS_NAME} == "osx" ]]; then
    echo "Attempting to build $project for OS X"
    /Applications/Unity/Unity.app/Contents/MacOS/Unity \
        -batchmode \
        -nographics \
        -silent-crashes \
        -logFile `pwd`/unity.log \
        -projectPath `pwd` \
        -executeMethod UnityTest.Batch.RunUnitTests \
        -executeMethod UnityTest.Batch.RunIntegrationTests \
        -testscenes=PlayerTestScene \
        -quit
    fail=$?
elif [[ ${TRAVIS_OS_NAME} == "linux" ]]; then
    echo "Attempting to build $project for Linux"
    /Applications/Unity/Unity.app/Contents/MacOS/Unity \
        -batchmode \
        -nographics \
        -silent-crashes \
        -logFile `pwd`/unity.log \
        -projectPath `pwd` \
        -executeMethod UnityTest.Batch.RunUnitTests \
        -executeMethod UnityTest.Batch.RunIntegrationTests \
        -testscenes=PlayerTestScene \
        -quit
    fail=$?
fi
echo 'Logs from build'
cat `pwd`/unity.log
exit $fail
