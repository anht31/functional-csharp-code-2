function hasPairWithSum(arr, sum) {
	const mySet = new Set();
	for (let i = 0; i < arr.length; i++) {
		if (mySet.has(arr[i])) {
			return true;
		}
		mySet.add(sum - arr[i]);
	}
	return false;
}

console.log(hasPairWithSum([6, 3], 9))