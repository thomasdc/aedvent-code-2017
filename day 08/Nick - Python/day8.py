import operator

def get_operator_fn(op):
    return {
        'inc' : operator.add,
        'dec' : operator.sub,
        '<' : operator.lt,
        '<=' : operator.le,
        '==' : operator.eq,
        '!=' : operator.ne,
        '>=' : operator.ge,
        '>' : operator.gt
        }[op]

def evaluate(op1, oper, op2):
    op1,op2 = int(op1), int(op2)
    return get_operator_fn(oper)(op1, op2)

lines = [line.rstrip('\n').replace('if','').split() for line in open('day8.txt')]

variables = {}
max_value = 0
for line in lines:
    if line[0] not in variables:
        variables[line[0]] = 0
    if line [3] not in variables:
        variables[line[0]] = 0
for line in lines:
    if evaluate(variables[line[3]], line[4], line[5]):
        variables[line[0]] = evaluate( variables[line[0]], line[1], line[2])
        if variables[line[0]] > max_value:
            max_value = variables[line[0]]
print(variables[max(variables, key=variables.get)])
print(max_value)