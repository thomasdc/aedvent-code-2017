import operator

class Register:
    def __init__(self, name):
        self.name = name
        self.place = 0

class Jumps:
    def __init__(self, reg, operator_fn, jump, child, logic, value):
        self.reg = reg
        self.operator_fn = operator_fn
        self.jump = int(jump)
        self.child = child
        self.logic = logic
        self.value = int(value)

# stack https://stackoverflow.com/questions/1740726/python-turn-string-into-operator
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

def read_input(path):
    split_txt = [line.rstrip('\n').replace('if', '').split() for line in open(path)]
    return split_txt

input_puzzle = read_input('day 08/Oliver - Python/input.txt')

register = []
unique_names = {objects[0] for objects in input_puzzle}
for unique_name in unique_names:
    register.append(Register(unique_name))

jumps = []
for element in input_puzzle:
    reg = [n for n in register if n.name == element[0]]
    operator_fn = get_operator_fn(element[1])
    jump = element[2]
    child = [n for n in register if n.name == element[3]]
    logic =  get_operator_fn(element[4])
    value = element[5]
    jumps.append(Jumps(reg, operator_fn, jump, child, logic, value))

# Go
maximum = 0
for j in jumps:
    if (j.logic(j.child[0].place, j.value)):
       j.reg[0].place = j.operator_fn(j.reg[0].place, j.jump)
        if j.reg[0].place > maximum:
            maximum = j.reg[0].place

max([r.place for r in register])
maximum