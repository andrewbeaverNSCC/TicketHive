// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const box = document.getElementById('resizableBox');
  const img = document.getElementById('resizableImg');
  let aspect = 16 / 9; // fallback default

  // Set the aspect ratio and size the container to it
  img.addEventListener('load', () => {
    if (img.naturalWidth && img.naturalHeight) {
      aspect = img.naturalWidth / img.naturalHeight;
      box.style.aspectRatio = aspect;
    }
  });

// https://developer.mozilla.org/en-US/docs/Web/API/ResizeObserver

// Create a ResizeObserver
// when the box width changes, set its height to maintain the current aspect ratio(aspect is set from the image load)
  let resizing = false;
  const observer = new ResizeObserver(() => {
    if (resizing) return;
    resizing = true;

    const newWidth = box.offsetWidth;
    const newHeight = newWidth / aspect;
    box.style.height = `${newHeight}px`;

    resizing = false;
  });

  observer.observe(box);
