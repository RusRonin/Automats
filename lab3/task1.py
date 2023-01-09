import sys
from common import *


def read_rules(input):
    rules = list(map(lambda x: x.split(" -> "), input))
    rules = list(map(lambda x: [x[0], x[1].split(" | ")], rules))
    result = []
    for rule in rules:
        for position in rule[1]:
            result.append([rule[0], position])
    return numpy.array(result)


def rules_to_array(rules, left_grammar):
    result = list(map(lambda x: list(x), rules[:,1]))
    if left_grammar:
        for i, rule in enumerate(result):
            if len(rule) == 1:
                rule.insert(0, '_')
            rule.append(rules[:,0][i])
    else:
        for i, rule in enumerate(result):
            if len(rule) == 1:
                rule.append('_')
            rule.insert(0, rules[:,0][i])
    return result


def get_signals(machine, final_state):
    signals = numpy.zeros((1, machine.shape[1]), machine.dtype)
    for j in range(1, len(machine[0])):
        if final_state == machine[0][j]:
            signals[0, j] = 'F'
    return signals


def build_undetermined_machine(rules, is_left_grammar):
    machine = numpy.zeros((1,1), '<U25')
    if is_left_grammar:
        states_queue = ['_']
    else:
        states_queue = [rules[0][0]]
    processed_states = []
    processed_inputs = []
    while len(states_queue) != 0:
        state = states_queue.pop(0)
        if state not in processed_states:
            machine = numpy.hstack((machine, numpy.zeros((machine.shape[0], 1), machine.dtype)))
            machine[0, -1] = state
            processed_states.append(state)
            for rule in rules:
                if rule[0] == state:
                    if rule[1] not in processed_inputs:
                        machine = numpy.vstack((machine, numpy.zeros((1, machine.shape[1]), machine.dtype)))
                        machine[-1, 0] = rule[1]
                        processed_inputs.append(rule[1])
                    row = numpy.where(machine[:,0] == rule[1])[0][0]
                    column = numpy.where(machine[0] == rule[0])[0][0]
                    if machine[row, column] == '':
                        machine[row, column] = rule[2]
                    else:
                        machine[row, column] = machine[row, column] + ',' + rule[2]
                    if (rule[2] not in states_queue) and (rule[2] not in processed_states):
                        states_queue.append(rule[2])
    if is_left_grammar:
        final_state = rules[0][0]
    else:
        final_state = '_'
    signals = get_signals(machine, final_state)
    machine = numpy.vstack((signals, machine))
    return machine


def process_left_grammar(input):
    rules_from_input = read_rules(input)
    rules = rules_to_array(rules_from_input, True)
    machine = build_undetermined_machine(rules, True)
    return to_determined_machine(machine)


def process_right_grammar(input):
    rules_from_input = read_rules(input)
    rules = rules_to_array(rules_from_input, False)
    machine = build_undetermined_machine(rules, False)
    return to_determined_machine(machine)


if __name__ == '__main__':
    if len(sys.argv) != 4:
        print('incorrect argument count')
        sys.exit()
    grammar_type = sys.argv[1]
    if grammar_type != "left" and grammar_type != "right":
        print('unknown grammar type')
        sys.exit()
    with open(sys.argv[2]) as file:
        input = file.read().splitlines()
    if grammar_type == 'left':
        output = process_left_grammar(input)
    else:
        output = process_right_grammar(input)
    numpy.savetxt(sys.argv[3], output, delimiter=';', fmt='%s')
