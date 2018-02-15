grammar TemplateTextLine;

template_text_line
    : text_line EOF
    ;

text_line
	: raw_part text_line          #rawPartExp
	| variable_usage text_line    #varUsageExp
    | /*epsilon*/                 #epsilonExp
    ;

raw_part
    : RAW_PART
    | RAW_CHARACTER
    ;

variable_usage
    : VAR_USAGE
    | EXT_VAR_USAGE
    ;

RAW_PART: ~('@') ~('@')* ;
EXT_VAR_USAGE : '@' '[' ~('@' | ']') ~('@' | ']')* ']' ;
fragment ID : [a-zA-Z] [a-zA-Z0-9]* ;
VAR_USAGE: '@' ID ;
RAW_CHARACTER : '@' ;