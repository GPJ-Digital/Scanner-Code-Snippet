 const keystrokeBuffer = [];
    let timerId;

    // Function to start buffering keystrokes
    function startBuffering() {
      document.addEventListener('keydown', handleKeyPress);
      timerId = setInterval(processBuffer, 1000); // Process buffer every 1000 milliseconds (1 second)
    }

    // Function to handle key presses and store them in the buffer
    function handleKeyPress(event) {
      const key = event.key;
      keystrokeBuffer.push(key);
    }

    // Function to process the buffered keystrokes
    function processBuffer() {
      if (keystrokeBuffer.length > 0) {
        const bufferText = keystrokeBuffer.join('');
        const bufferElement = document.getElementById('buffer');
        bufferElement.value += bufferText + '\n';
        keystrokeBuffer.length = 0; // Clear the buffer
      }
    }

    // Start buffering when the page loads
    window.onload = startBuffering;