cd /c/_Application/StatefulT4Processor
/bin/git stash
/bin/git checkout -b $1
/bin/git stash pop
/bin/git commit -am 'Machine generated commit'
/bin/git push $2 $1 
read