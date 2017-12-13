# Tree traversal
# http://blog.nextgenetics.net/?e=64
lines = [line.rstrip('\n').replace('<->','').replace(',','').split() for line in open('day 12/Oliver - Python/input.txt')]
# Make graph
graph = {}
for line in lines:
    graph[line[0]] = line[1:]
# find path function
def find_path(graph, start, end, path=[]):
    path = path + [start]
    if start == end:
        return path
    if start not in graph:
        return None
    for node in graph[start]:
        if node not in path:
            newpath = find_path(graph, node, end, path)
            if newpath:
                return newpath
    return None
#test
find_path(graph, '3', '0')
# get result from each startpoint in the graph to 0
result = []
for start in graph:
    result.append(find_path(graph, start, '0'))
# which startpoint is not connected to 0
len([n for n in result if n != None])



