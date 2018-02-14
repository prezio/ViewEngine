grammar TemplateScope;

template_scope
    : template_statement EOF
    ;

template_statement
    : CODE_LINE template_statement           #codeLineExp
    | COMMENT_LINE template_statement        #commentLineExp
    | VAR_USAGE template_statement           #varUsageExp
    | template_raw_text template_statement   #rawLineExp
    | /*epsilon*/                            #epsilonExp
    ;

template_raw_text
    : RAW_TEXT
    | RAW_TEXT2
    ;

fragment TEXT_LINE : ~( '\r' | '\n' )* ;
CODE_LINE : '--' TEXT_LINE ;
COMMENT_LINE : '//' TEXT_LINE ;
RAW_TEXT : ~( '\r' | '\n' | '@' ) ~( '\r' | '\n' | '@' )* ;
fragment ID : [a-zA-Z] [a-zA-Z0-9]* ;
fragment VAR_REMAINDER : '.' ID ('(' ID VAR_REMAINDER*')')? ;
VAR_USAGE: '@' ID VAR_REMAINDER*;
RAW_TEXT2: '@' ;

WS : [ \r\t\n]+ -> skip ;