//Langue Save to Local Storage
function setLanguage(lang) {
    console.log("Language setting to:", lang);  // 此处输出调试信息
    localStorage.setItem('preferredLanguage', lang);
    applyLanguage(lang);
}

// Apply Language to Current Web
function applyLanguage(lang) {
    var elements = document.querySelectorAll('[data-translate]');
    elements.forEach(element => {
        var key = element.getAttribute('data-translate');
        if (langData[lang] && langData[lang][key]) {
            element.innerHTML = langData[lang][key];  // Use innerHTML to include HTML formatting
        } else {
            console.error("Missing translation for key:", key);  // Error logging if translation key is missing
        }
    });
}

// Load the Language Preferences
document.addEventListener('DOMContentLoaded', () => {
    var preferredLanguage = localStorage.getItem('preferredLanguage') || 'en';
    applyLanguage(preferredLanguage);
});

