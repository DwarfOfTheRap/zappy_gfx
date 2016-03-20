NAME := gfx.app
PATH := $(shell pwd)/
BIN := /Applications/Unity/Unity.app/Contents/MacOS/Unity
FLAGS := -batchmode -quit -nographics -buildOSXPlayer
LOGFLAG := -logFile
LOGFILE := unity.log

all: $(NAME)

$(NAME):
	$(BIN) $(FLAGS) $(PATH)$(NAME) $(LOGFLAG) $(PATH)$(LOGFILE)

clean:

fclean:
	rm -rf $(NAME)

re: fclean all

.PHONY: all clean fclean re
