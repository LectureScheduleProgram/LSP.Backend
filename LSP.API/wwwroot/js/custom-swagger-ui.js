document.addEventListener("DOMContentLoaded", function () {
    setTimeout(function () {
        const mainElement = document.querySelector('.info .main')

        if (mainElement) {
            const h2Element = mainElement.querySelector('h2')
            const aElement = mainElement.querySelector('a')

            if (h2Element && aElement) {
                // Create and configure the <pre> element
                const preElement = document.createElement('pre')
                preElement.classList.add('base-url') // Add the 'base-url' class
                const preText = document.createTextNode('[ Base URL: api.LectureSchedule.com ]')
                preElement.appendChild(preText)

                // Insert the <pre> element between <h2> and <a>
                mainElement.insertBefore(preElement, aElement)
            }
        }
    }, 1000) // Adjust the timeout value as needed
})
