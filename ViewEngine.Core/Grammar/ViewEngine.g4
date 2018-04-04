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
    | mixin_usage regular_statement             #mixinUsageExp
	| assignment_exp regular_statement          #assignmentExp
	| eval_exp regular_statement                #evaluateExp
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



// grammar for assignment

assignment_exp
	: VAR ID EQUAL TEMPLATE_SCOPE
    | VAR ID EQUAL REGULAR_SCOPE
    | VAR ID EQUAL TEXT_STRING
	| VAR ID EQUAL CODE_SCOPE
	| VAR ID EQUAL ID
    ;

// end of grammar for assignment



// grammar for expression evaluation

eval_exp
	: EVAL ID
	;

// end of grammar for expression evaluation



// grammar for mixin declarations

mixin_declarations
	: COMMENT_LINE mixin_declarations
    | mixin_declaration mixin_declarations
	| /*epsilon*/
	;

// end of grammar for mixin declarations



// grammar for mixin usage

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
    : TEMPLATE_SCOPE
    | REGULAR_SCOPE
    | TEXT_STRING
	| CODE_SCOPE
	| ID
    ;

// end of grammar for mixin usage



// grammar for mixin declaration

mixin_declaration
    : MIXIN ID LP mixin_declaration_params RP
        mixin_body
    ;

mixin_declaration_params
	: mixin_declaration_params2
	| /*espilon*/
	;

mixin_declaration_params2
	: ID COMMA mixin_declaration_params2
	| ID
	;

mixin_body
    : REGULAR_SCOPE
    | TEMPLATE_SCOPE
    ;

// end of grammar for mixin declaration



// Tokens declaration

MIXIN : 'mixin' ;
MODEL : 'model' ;
USING : 'using' ;
VAR : 'var' ;
EVAL : 'eval' ;
ID : [a-zA-Z] [a-zA-Z0-9.]* ;
COMMA : ',' ;
LP : '(' ;
RP : ')' ;
EQUAL : '=' ;

TEMPLATE_SCOPE : '<-' ( ~'-' | ( '-'+ ~[>*]) )* '-'* '->';
REGULAR_SCOPE : '<|' (REGULAR_SCOPE | ~'|' | ( '|'+ ~[>*]) )* '|>';
CODE_SCOPE : '@{' ~('}')* '}' ;
TEXT_STRING : '"' ~('"')* '"' ;
fragment TEXT_LINE : ~( '\r' | '\n' )* ;
CODE_LINE : '--' TEXT_LINE ;
COMMENT_LINE : '//' TEXT_LINE ;


WS : [ \r\t\n]+ -> skip ;