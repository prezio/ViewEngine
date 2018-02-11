grammar ViewEngine;

// grammar for main file

main
    : models_introduction regular_statement
    ;

// grammar for secondary file

secondary
    : regular_statement
    ;


// grammar for regular statement declaration

regular_statement
    : CODE_LINE regular_statement               #codeLineExp
    | COMMENT_LINE regular_statement            #commentLineExp
    | REGULAR_SCOPE regular_statement           #rawScopeExp
    | func_usage SEP regular_statement          #funcUsageExp
    | func_declaration regular_statement        #funcDeclExp
    | SEP regular_statement                     #emptyExp
    | EOF                                       #eofExp
    | /*epsilon*/                               #epsilonExp
    ;



// grammar for model introduction

models_introduction
    : COMMENT_LINE models_introduction
    | model_introduction SEP models_introduction
    | /*epsilon*/
    ;

model_introduction
	: VARID ID
	;

// end of grammar for model introduction



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
    : VARID EQUAL TEMPLATE_SCOPE
    | VARID EQUAL REGULAR_SCOPE
    | VARID EQUAL TEXT_STRING
    ;

// end of grammar for function usage



// grammar for function declaration

func_declaration
    : FUNCTION ID LP func_decl_args RP 
        func_body
    ;

func_body
    : REGULAR_SCOPE
    | TEMPLATE_SCOPE
    ;

func_decl_args
	: func_decl_args2
	| /*epsilon*/
	;

func_decl_args2
    : VARID
    | VARID COMMA func_decl_args2
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
EQUAL : '=' ;

TEMPLATE_SCOPE : '<@' ( ~'@' | ( '@'+ ~[>*]) )* '@'* '@>';
REGULAR_SCOPE : '<|' ( ~'|' | ( '|'+ ~[>*]) )* '|'* '|>';
TEXT_STRING : '"' ~('"')* '"' ;
fragment TEXT_LINE : ~( '\r' | '\n' )* ;
CODE_LINE : '--' TEXT_LINE ;
COMMENT_LINE : '//' TEXT_LINE ;


WS : [ \r\t\n]+ -> skip ;