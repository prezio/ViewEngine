grammar TemplateScope;

template_scope
    : template_statement EOF
    ;

template_statement
    : CODE_LINE template_statement           #codeLineExp
    | COMMENT_LINE template_statement        #commentLineExp
    | TEMPLATE_LINE template_statement       #templateLineExp
    | /*epsilon*/                            #epsilonExp
    ;


fragment TEXT_LINE : ~( '\r' | '\n' )* ;
CODE_LINE : '--' TEXT_LINE ;
COMMENT_LINE : '//' TEXT_LINE ;
TEMPLATE_LINE : ~( '\r' | '\n' ) ;

WS : [ \r\t\n]+ -> skip ;