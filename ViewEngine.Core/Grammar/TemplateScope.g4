grammar TemplateScope;

template_scope
    : template_statement EOF
    ;

template_statement
    : CODE_LINE template_statement           #codeLineExp
    | COMMENT_LINE template_statement        #commentLineExp
    | RAW_TEXT_LINE template_statement       #rawLineExp
    | /*epsilon*/                            #epsilonExp
    ;

fragment TEXT_LINE : ~( '\r' | '\n' )* ;
CODE_LINE : '--' TEXT_LINE ;
COMMENT_LINE : '//' TEXT_LINE ;
RAW_TEXT_LINE : ~( '\r' | '\n' ) ~( '\r' | '\n' )* ;

WS : [ \r\t\n]+ -> skip ;
NL: '\r'? '\n' -> skip ;