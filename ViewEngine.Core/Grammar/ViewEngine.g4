grammar ViewEngine;

// grammar for main file

main
    : models_introduction declarations regular_statement
    ;

// grammar for secondary file

secondary
    : declarations
    ;


// grammar for regular statement declaration

regular_statement
    : CODE_LINE regular_statement               #codeLineExp
    | COMMENT_LINE regular_statement            #commentLineExp
    | TEMPLATE_SCOPE regular_statement          #templateScopeExp
    | mixin_usage SEP regular_statement         #mixinUsageExp
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
	: MODEL ID ID
	;

// end of grammar for model introduction



// grammar for declarations

declarations
	: mixin_declaration declarations
	| SEP declarations
	| /*epsilon*/
	;

// end of grammar for declarations



// grammar for function usage

mixin_usage
    : ID LP mixin_usage_args RP
    ;

mixin_usage_args
    : mixin_usage_args2
    | /*epsilon*/
    ;

mixin_usage_args2
    : mixin_usage_param
    | mixin_usage_param COMMA mixin_usage_args2
    ;

mixin_usage_param
    : ID EQUAL TEMPLATE_SCOPE
    | ID EQUAL REGULAR_SCOPE
    | ID EQUAL TEXT_STRING
	| ID EQUAL CODE_SCOPE
	| ID EQUAL ID
    ;

// end of grammar for function usage



// grammar for function declaration

mixin_declaration
    : MIXIN ID COLON
        mixin_body
    ;

mixin_body
    : REGULAR_SCOPE
    | TEMPLATE_SCOPE
    ;

// end of grammar for function declaration



// Tokens declaration

MIXIN : 'mixin' ;
MODEL : 'model' ;
ID : [a-zA-Z] [a-zA-Z0-9]* ;
COMMA : ',' ;
COLON : ':' ;
SEP : ';' ;
LP : '(' ;
RP : ')' ;
EQUAL : '=' ;

TEMPLATE_SCOPE : '<-' ( ~'-' | ( '-'+ ~[>*]) )* '-'* '->';
REGULAR_SCOPE : '<|' ( ~'|' | ( '|'+ ~[>*]) )* '|'* '|>';
CODE_SCOPE : '@{' ~('}')* '}' ;
TEXT_STRING : '"' ~('"')* '"' ;
fragment TEXT_LINE : ~( '\r' | '\n' )* ;
CODE_LINE : '--' TEXT_LINE ;
COMMENT_LINE : '//' TEXT_LINE ;


WS : [ \r\t\n]+ -> skip ;