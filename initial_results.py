import matplotlib.pyplot as plt

with open("test_output.txt","r") as f:
    data = [line.strip().split() for line in f.readlines()]

fig = plt.figure()
ax = fig.add_subplot(111)
ax.xaxis.set_visible(False)
ax.yaxis.set_visible(False)
col_labels=("Test", "Expected Output", "Actual Output")
table = ax.table(cellText=data,
          colLabels=col_labels,
          loc='center')

plt.savefig("initial_results.png")
plt.show()


