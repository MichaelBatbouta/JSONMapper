import json

def get_height(obj):
    """
    Returns the height of a dictionary object
    i.e. the number of keys
    """
    if isinstance(obj, dict):
        return len(obj.keys())
    elif isinstance(obj, list):
        return len(obj)
    else:
        return None

def get_depth(obj):
    """
    Returns how deeply nested a dictionary object is
    """
    if isinstance(obj, dict):
        return traverse_dict(obj, 1)
    elif isinstance(obj, list):
        return traverse_list(obj, 1)
    else:
        return None

def traverse_dict(input_dict, counter):
    """
    Returns the maximum depth of key-value pairs in a dict
    """
    depths = []
    for value in input_dict.values():
        if isinstance(value, dict):
            depths.append(traverse_dict(value, counter+1))
        elif isinstance(value, list):
            depths.append(traverse_list(value, counter+1))
    if depths:
        return max(depths)
    return counter

def traverse_list(input_list, counter):
    """
    Returns the maximum depth of objects in a list
    """
    depths = []
    for element in input_list:
        if isinstance(element, dict):
            depths.append(traverse_dict(element, counter+1))
        elif isinstance(element, list):
            depths.append(traverse_list(element, counter+1))
    if depths:
        return max(depths)
    return counter

test_dict1 = {   "color": "blue", 
                "number": 54,
                "next": {
                    "color": "blue", 
                    "number": 54,
                    }
            }

print(get_depth(test_dict1))
