lines = [line.rstrip('\n').replace('(','').replace(')','').replace('->','').replace(',','').split() for line in open('day7.txt')]

nodes = {}
for line in lines:
	for i, element in enumerate(line):
		if i == 0:
			nodes[element] = {'id': element, 'weight': int(line[1]), 'children' : [], 'childrenweight': 0}
		if i > 1:
			nodes[line[0]]['children'].append(element)
for node in nodes:
	found = 0
	for othernode in nodes:
		if node in nodes[othernode]['children']:
			found = 1
			break
	if found == 0:
		print(node)

for node in nodes:
	if len(nodes[node]['children']) ==0:
		nodes[node]['childrenweight'] = int(nodes[node]['weight'])

keeplooping = 1
while keeplooping:
	for node in nodes:
		if len(nodes[node]['children']) != 0 and keeplooping:
			if len([x for x in nodes[node]['children'] if nodes[x]['childrenweight'] == 0]) == 0: 
				nodes[node]['childrenweight'] = sum([nodes[x]['childrenweight'] for x in nodes[node]['children']]) + nodes[node]['weight']
				weightlist = [nodes[child]['childrenweight'] for child in nodes[node]['children']]
				weight = max(set(weightlist),key=weightlist.count)
				if len(weightlist) != 1:
					for child in nodes[node]['children']:
						if nodes[child]['childrenweight'] != weight:
							print(weight - nodes[child]['childrenweight'] + nodes[child]['weight'])				
							keeplooping = 0
						

