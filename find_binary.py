import os
import sys
import json

results = []

def find(directory, query, skips=set()):
    global results
    for p in os.listdir(directory):
        q = os.path.abspath(os.path.join(directory, p))
        if p.startswith("."):
            continue
        if q in skips:
            continue
        if os.path.isdir(q):
            find(q, query, skips=skips)
        else:
            with open(q, 'rb') as f:
                bts = f.read(len(query) // 2)
                while len(bts) == len(query) // 2:
                    h = bts.hex()
                    if query.lower() == h:
                        print("Found a result in: " + q + " at: " + hex(f.tell()))
                        results.append({"path": q, "location": hex(f.tell())})
                        # break
                    f.seek(4 - len(query) // 2, 1)
                    bts = f.read(len(query) // 2)
            print("Finished file: " + q)

if __name__ == "__main__":
    s = set([os.path.abspath(os.path.join(sys.argv[1], item)).replace("\\\\", "\\") for item in sys.argv[3:]])
    find(sys.argv[1], sys.argv[2], skips=s)
    print("==================================================================")
    with open("results.json", 'w') as f:
        json.dump(results, f, indent=4)
    print("Found: %i matches" % len(results))
    # for item in results:
    #     print(item)