
function MergeSort(array) {
    if (array.length <= 1) return array

    // split array into left and right
    const length = array.length
    const half = Math.floor(length / 2)
    const left = array.slice(0, half)
    const right = array.slice(half)

    return merge(
        MergeSort(left),
        MergeSort(right)
    )
}

function merge(left, right) {
    // to compare
    let result = []
    if (left.length === 0) return right
    if (right.length === 0) return left

    let l = 0
    let r = 0
    while (left[l] !== undefined || right[r] !== undefined) {
        if (left[l] <= right[r] || right[r] === undefined) {
            result.push(left[l++])
        } else {
            result.push(right[r++])
        }
    }

    //console.log(result)
    return result
}

class App {
    run() {
        const numbers = [99, 44, 6, 2, 1, 5, 63, 87, 283, 4, 0];
        //const numbers = [1, 2, 3, 8, 5, 6, 7, 9, 11, 12, 10]
        const answer = MergeSort(numbers);
        console.log(answer);
    }
}

export { App }