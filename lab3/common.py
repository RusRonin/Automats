import numpy


def get_ecloses(transitions):
    ecloses = {}
    for state in transitions:
        ecloses[state[0]] = [state[0]]
        if len(state[1]) != 0:
            for transition in state[1].split(','):
                if transition not in ecloses[state[0]]:
                    ecloses[state[0]].append(transition)
    completed = False
    while not completed:
        completed = True
        for key in ecloses:
            for state in ecloses[key]:
                for transition in ecloses[state]:
                    if transition not in ecloses[key]:
                        ecloses[key].append(transition)
                        completed = False
    for key in ecloses:
        ecloses[key].sort()
    return ecloses


def build_determined_machine(source_undetermined_machine, ecloses):
    determined_machine = numpy.zeros((source_undetermined_machine.shape[0] - 1, 1), source_undetermined_machine.dtype)
    for i, line in enumerate(source_undetermined_machine[1:]):
        determined_machine[i] = line[0]
    undetermined_machine_state_signals = source_undetermined_machine[1::-1, 1:]
    undetermined_machine_states = source_undetermined_machine[1, 1:]
    input_chars = source_undetermined_machine.T[0, 2:]
    undetermined_machine_without_indexes = source_undetermined_machine[2:, 1:]
    initial_undetermined_machine_state = undetermined_machine_states[0]
    initial_determined_machine_state = ecloses[initial_undetermined_machine_state]
    states_to_process = [initial_determined_machine_state]
    states_done = []
    while len(states_to_process) != 0:
        determined_machine_state = states_to_process.pop(0)
        if determined_machine_state not in states_done:
            states_done.append(determined_machine_state)
            new_column = numpy.zeros((determined_machine.shape[0], 1), determined_machine.dtype)
            new_column[0, 0] = ''.join(determined_machine_state)
            for char_idx in range(len(input_chars)):
                new_state = []
                for original_state in determined_machine_state:
                    original_state_idx = numpy.where(undetermined_machine_states == original_state)[0][0]
                    original_transitions = undetermined_machine_without_indexes[char_idx, original_state_idx]
                    if len(original_transitions) != 0:
                        new_state.extend(original_transitions.split(','))
                if len(new_state) != 0:
                    state_with_ecloses = []
                    for target in new_state:
                        for state in ecloses[target]:
                            if state not in state_with_ecloses:
                                state_with_ecloses.append(state)
                    state_with_ecloses.sort()
                    states_to_process.append(state_with_ecloses)
                    new_column[char_idx + 1, 0] = ''.join(state_with_ecloses)
            determined_machine = numpy.hstack((determined_machine, new_column))
    state_labels = {}
    for i in range(len(states_done)):
        state_labels[''.join(states_done[i])] = 'S' + str(i)
    for i in range(len(determined_machine)):
        for j in range(1, len(determined_machine[0])):
            if len(determined_machine[i, j]) != 0:
                determined_machine[i, j] = state_labels[determined_machine[i, j]]
    signals = numpy.zeros((1, determined_machine.shape[1]), determined_machine.dtype)
    for i in range(len(states_done)):
        signal = ''
        for elem in states_done[i]:
            idx = numpy.where(undetermined_machine_state_signals[0] == elem)[0][0]
            signal = signal + undetermined_machine_state_signals[1, idx]
        signals[0, i + 1] = signal
    determined_machine = numpy.vstack((signals, determined_machine))
    return state_labels, determined_machine


def to_determined_machine(undetermined_machine, empty_char='e'):
    empty_char_row_idx_arr = numpy.where(undetermined_machine[:, 0] == empty_char)[0]
    if len(empty_char_row_idx_arr) == 0:
        empty_char_transitions_row = numpy.zeros(undetermined_machine[1, 1:].shape, undetermined_machine.dtype)
        empty_transitions = numpy.vstack((undetermined_machine[1, 1:], empty_char_transitions_row)).T
    else:
        empty_char_transitions_row = numpy.where(undetermined_machine[:, 0] == empty_char)[0][0]
        empty_transitions = numpy.vstack((undetermined_machine[1, 1:], undetermined_machine[empty_char_transitions_row, 1:])).T
        undetermined_machine = numpy.delete(undetermined_machine, empty_char_transitions_row, 0)
    ecloses = get_ecloses(empty_transitions)
    state_labels, determined_machine = build_determined_machine(undetermined_machine, ecloses)
    return determined_machine
