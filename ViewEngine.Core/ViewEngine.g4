grammar ViewEngine;


// grammar for statement declaration, main program

statement
    : CODE_LINE statement               #codeLineExp
    | COMMENT_LINE statement            #commentLineExp
    | func_usage SEP statement          #funcUsageExp
    | func_declaration SEP statement    #funcDeclExp
    | EOF                               #eofExp
    ;



// grammar for function usage

func_usage
    : ID LP func_usage_args RP
    ;

func_usage_args
    : func_usage_args2
    | /*epsilon*/
    ;

func_usage_args2
    : func_usage_param
    | func_usage_param COMMA func_usage_args2
    ;

func_usage_param
    : VARID
    | TEXT_STRING
    ;

// end of grammar for function usage



// grammar for function declaration

func_declaration
    : FUNCTION ID LP func_decl_args RP 
        LS func_body RS
    ;

func_decl_args
    : func_decl_args2
    | /*epsilon*/
    ;

func_decl_args2
    : func_decl_param
    | func_decl_param COMMA func_decl_args2
    ;

func_decl_param
    : VARID ID
    ;

func_body
    : COMMENT_LINE func_body
    | CODE_LINE func_body
    | FUNC_BODY_LINE func_body
    | /*epsilon*/
    ;

// end of grammar for function declaration



// Tokens declaration

FUNCTION : 'function' ;
ID : [a-zA-Z] [a-zA-Z0-9]* ;
VARID : '@' [a-zA-Z] [a-zA-Z0-9]* ;
COMMA : ',' ;
SEP : ';' ;
LP : '(' ;
RP : ')' ;
LS : '{' ;
RS : '}' ;

TEXT_STRING : '"' ~('"')* '"' ;
fragment TEXT_LINE : ~( '\r' | '\n' )* ;
CODE_LINE : '--' TEXT_LINE ;
COMMENT_LINE : '//' TEXT_LINE ;
FUNC_BODY_LINE : '|' TEXT_LINE ;

WS : [ \r\t\n]+ -> skip ;