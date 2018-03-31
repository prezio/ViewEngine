grammar ViewEngine;

// grammar for main file

main
    : using_namespace model_introduction mixin_declarations regular_statement
    ;

// grammar for secondary file

secondary
    : using_namespace mixin_declarations
    ;


// grammar for regular statement declaration

regular_statement
    : CODE_LINE regular_statement               #codeLineExp
    | COMMENT_LINE regular_statement            #commentLineExp
    | TEMPLATE_SCOPE regular_statement          #templateScopeExp
    | mixin_usage regular_statement				#mixinUsageExp
    | EOF                                       #eofExp
    | /*epsilon*/                               #epsilonExp
    ;

// end of grammar for regular statement declaration



// grammar for using namespace

using_namespace
	: COMMENT_LINE using_namespace
	| USING CODE_SCOPE using_namespace
	| /*epsilon*/
	;

// end of grammar for using namespace



// grammar for model introduction

model_introduction
    : COMMENT_LINE model_introduction
    | MODEL CODE_SCOPE
    | /*epsilon*/
    ;

// end of grammar for model introduction



// grammar for declarations

mixin_declarations
	: COMMENT_LINE mixin_declarations
    | mixin_declaration mixin_declarations
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
    : MIXIN ID
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
USING : 'using' ;
ID : [a-zA-Z] [a-zA-Z0-9.]* ;
COMMA : ',' ;
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