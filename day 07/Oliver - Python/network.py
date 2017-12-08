# Read file and parse it into list
def read_input(path):
    input_txt = [line.strip() for line in open(path)]
    split_txt = [line.rstrip('\n').replace('(', '').replace(')', '').replace('->', '').replace(',', '').split() for line in input_txt]
    return split_txt

# Part ONE
def root(input):
    result = {first[0]: 0 for first in input}
    for elone in input:
        if any(elone[0] in item[1:] for item in input):
            result[elone[0]] += 1
    root = print(dict((v, k) for k, v in result.items()).get(0))
    return root

# get root part one
root(read_input('day 07/Oliver - Python/input.txt'))

# Part two

class Node:
    def __init__(self, name, weight, refs):
        self.name = name
        self.weight = weight
        self.refs = refs
        self.children = []
        self.parent = None

    def totalweight(self):
        return self.weight + sum([c.totalweight() for c in self.children])

    def balanced(self):
        if len(self.children) == 0:
            return True
        return all(c.totalweight() == self.children[0].totalweight() for c in self.children)

    def frombottom(self):
        if self.parent == None:
            return 0
        return self.parent.frombottom() + 1

def make_nodes(input):
    nodes = []
    for lines in input:
        name = lines[0]
        weight = int(lines[1])
        refs = []
        if len(lines) > 2:
            refs = lines[2:]
        # print(name, weight, refs)
        nodes.append(Node(name, weight, refs))
        # print(vars(nodes[0]))
    return nodes

def make_network(nodes_input):
    for node in nodes_input:
        for ref in node.refs:
            child_node = next((n for n in nodes_input if n.name == ref), None)
            if child_node != None:
                node.children.append(child_node)
                child_node.parent = node
    return nodes_input

def balance(nodes_input):
    unbalanced_nodes = [node for node in nodes_input if not node.balanced()]
    root_node = sorted(unbalanced_nodes, key = lambda u: u.frombottom())[-1]
    nodes_unbalanced = sorted(root_node.children, key = lambda c: c.totalweight())
    correct_weight = 0
    if nodes_unbalanced[0].totalweight() != nodes_unbalanced[1].totalweight():
        correct_weight = nodes_unbalanced[1].totalweight() - nodes_unbalanced[0].totalweight() + nodes_unbalanced[0].weight
    else:
        correct_weight = nodes_unbalanced[0].totalweight() - nodes_unbalanced[-1].totalweight() + nodes_unbalanced[-1].weight
    return correct_weight

balance(make_network(make_nodes(read_input('day 07/Oliver - Python/test.txt'))))
balance(make_network(make_nodes(read_input('day 07/Oliver - Python/input.txt'))))
