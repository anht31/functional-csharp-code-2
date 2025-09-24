
/**
 * @param {number[]} A
 * @param {number} digit
 * @param {number} radix
 */
function countingSort(A, digit, radix) {
    const n = A.length
    let B = new Array(n)
    let C = new Array(radix).fill(0)

    // step 1 - Counter digit
    for (let i = 0; i < n; i++) {
        let d = Math.floor(A[i] / Math.pow(radix, digit)) % radix
        C[d]++
    }

    // step 2 - Modify counter
    for (let i = 1; i < radix; i++) {
        C[i] += C[i - 1]
    }

    // step 3 - Right to Left, distribute
    for (let i = n - 1; i >= 0; i--) {
        let d = Math.floor(A[i] / Math.pow(radix, digit)) % radix
        B[--C[d]] = A[i]
    }

    console.log(B)

    return B

}

const logBase = (x, b) => Math.log(x) / Math.log(b)

function radixSort(A, radix) {
    if (A.length <= 1) return A
    const maxNumber = Math.max(...A)
    const digitLength = Math.floor(logBase(maxNumber, radix)) + 1

    let output = A
    for(let d = 0; d < digitLength; d++) {
        output = countingSort(output, d, radix)
    }

    return output
}

class App {
    run() {
        const arr = [170, 45, 75, 90, 802, 24, 2, 66];
        console.log(arr)
        // console.log(countingSort(arr, 0, 10))
        console.log(radixSort(arr, 10))
    }
}

export { App }