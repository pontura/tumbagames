FLEX?=$(HOME)/qb9/flex

ifdef WINDOWS
SUFFIX = .exe
endif

COMPC ?= $(FLEX)/bin/compc$(SUFFIX)

FLEX_VERSION=$(shell $(COMPC) --version)
ifeq (Version 4.6,$(findstring Version 4.6,$(FLEX_VERSION)))
$(warning Flex SDK $(FLEX_VERSION) -- Compiling for Flash Player 11)
FLASH_PLAYER_TARGET_OPTS = -define=CONFIG::FP10,false  -define=CONFIG::FP11,true
else ifeq (Version 4.5,$(findstring Version 4.5,$(FLEX_VERSION)))
$(warning Flex SDK $(FLEX_VERSION) -- Compiling for Flash Player 10.2)
FLASH_PLAYER_TARGET_OPTS = -target-player=10.2.0 -define=CONFIG::FP10,true -define=CONFIG::FP11,false
else
$(warning Flex SDK $(FLEX_VERSION) -- Compiling for Flash Player 10)
FLASH_PLAYER_TARGET_OPTS = -target-player=10.0.0 -define=CONFIG::FP10,true -define=CONFIG::FP11,false
endif

REVISION=$(shell svn info | grep -F 'Revision:' | sed -e 's/Revision: //')
TARGET=../swc/flashlib$(REVISION).swc

all: $(TARGET)

SOURCES=.
LIBS=../libs

$(TARGET): $(shell find . -name '*.as')
	$(COMPC) \
		-include-sources+=$(SOURCES) \
		-source-path+=$(SOURCES) \
		-source-path+=$(LIBS) \
		$(FLASH_PLAYER_TARGET_OPTS) \
		-output=$(TARGET)

clean:
	rm -f $(TARGET)

.PHONY: all clean
