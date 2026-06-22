// How to Add Copy to Clipboard Buttons to Code Blocks in Hugo - March 22, 2019  
// https://www.dannyguo.com/blog/how-to-add-copy-to-clipboard-buttons-to-code-blocks-in-hugo/

const btns = document.querySelectorAll('button[id^=copyscriptbutton]');
btns.forEach(btn => {

    btn.addEventListener('click', event => {
        var text = btn.nextElementSibling.innerText;
        navigator.clipboard.writeText(text);
        btn.textContent = 'Copied!';

        setTimeout(function () {
            btn.textContent = 'Copy!';
        }, 2000);

    });

});

// Resize the .tabs container to match the active tab content height
function updateTabsHeight() {
    document.querySelectorAll('.tabs').forEach(function(tabs) {
        var activeContent = tabs.querySelector('[type=radio]:checked ~ label ~ .content');
        if (activeContent) {
            tabs.style.minHeight = (28 + activeContent.offsetHeight) + 'px';
        }
    });
}

document.querySelectorAll('.tabs [type=radio]').forEach(function(radio) {
    radio.addEventListener('change', updateTabsHeight);
});

window.addEventListener('load', updateTabsHeight);
