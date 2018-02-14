grammar TemplateTextLine;

text_line
	: RAW_PART text_line          #rawPartExp
	| VAR_USAGE text_line         #varUsageExp
	| RAW_CHARACTER text_line     #rawCharacterExp

RAW_PART: ~('@')* ;
fragment ID : [a-zA-Z] [a-zA-Z0-9]* ;
fragment VAR_REMAINDER : '.' ID ('(' ID VAR_REMAINDER*')')? ;
VAR_USAGE: '@' ID VAR_REMAINDER* ;
RAW_CHARACTER : '@' ;
