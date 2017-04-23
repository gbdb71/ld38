COLOR='\033[0;36m'
NC='\033[0m' # No Color

echo " "

echo "${COLOR}Pushing build for OSX${NC}"
butler push Builds/osx anttihaavikko/hafd:osx

echo "${COLOR}Pushing build for Windows${NC}"
butler push Builds/win anttihaavikko/hafd:win

echo "${COLOR}Pushing build for Linux${NC}"
butler push Builds/linux anttihaavikko/hafd:linux

echo "${COLOR}Pushing build for HTML5${NC}"
butler push Builds/web anttihaavikko/hafd:html5
