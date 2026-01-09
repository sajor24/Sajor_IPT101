// Messages
const messages = [
    "You make every day brighter just by existing 💕",
    "Thinking of you always brings a smile to my face 😊",
    "You're the best thing that ever happened to me 💖",
    "Every moment with you is a treasure 🌟",
    "You're my favorite person in the whole world 🌍",
    "Your smile is my favorite view 😍",
    "I fall for you more every single day 💗",
    "You're the reason I believe in love 💝"
];

const surprises = [
    "You're absolutely amazing! 💕",
    "I'm so lucky to have you! 🍀",
    "You make my heart skip a beat! 💓",
    "You're my everything! 🌟",
    "I love you more than words can say! 💖"
];

// Anniversary date
const anniversaryDate = new Date(2025, 0, 14); // January 14, 2025

// Initialize
let musicPlaying = false;
const bgMusic = document.getElementById('bgMusic');

// Show random message on load
window.onload = function() {
    refreshMessage();
    updateCountdown();
    setInterval(updateCountdown, 1000);
};

function refreshMessage() {
    const randomMsg = messages[Math.floor(Math.random() * messages.length)];
    document.getElementById('dailyMessage').textContent = randomMsg;
}

function showSurprise() {
    const surpriseMsg = document.getElementById('surpriseMsg');
    const randomSurprise = surprises[Math.floor(Math.random() * surprises.length)];
    surpriseMsg.textContent = randomSurprise;
    surpriseMsg.classList.add('show');
    
    setTimeout(() => {
        surpriseMsg.classList.remove('show');
    }, 3000);
}

function showPage(page) {
    // Hide all pages
    document.getElementById('homePage').style.display = 'none';
    document.getElementById('lettersPage').style.display = 'none';
    document.getElementById('picturesPage').style.display = 'none';
    document.getElementById('countdownPage').style.display = 'none';
    
    // Show selected page
    if (page === 'home') {
        document.getElementById('homePage').style.display = 'block';
    } else if (page === 'letters') {
        document.getElementById('lettersPage').style.display = 'block';
    } else if (page === 'pictures') {
        document.getElementById('picturesPage').style.display = 'block';
    } else if (page === 'countdown') {
        document.getElementById('countdownPage').style.display = 'block';
    }
}

function toggleMusic() {
    const musicIcon = document.getElementById('musicIcon');
    const musicText = document.getElementById('musicText');
    
    if (musicPlaying) {
        bgMusic.pause();
        musicIcon.textContent = '🔇';
        musicText.textContent = 'Play Music';
        musicPlaying = false;
    } else {
        bgMusic.play();
        musicIcon.textContent = '🔊';
        musicText.textContent = 'Pause Music';
        musicPlaying = true;
    }
}

function updateCountdown() {
    const now = new Date();
    const startDate = new Date(2025, 0, 14); // January 14, 2025
    
    // Days together
    const daysTogether = Math.floor((now - startDate) / (1000 * 60 * 60 * 24));
    document.getElementById('daysTogether').textContent = Math.abs(daysTogether);
    
    // Next anniversary
    let nextAnniversary = new Date(now.getFullYear(), 0, 14);
    if (nextAnniversary < now) {
        nextAnniversary = new Date(now.getFullYear() + 1, 0, 14);
    }
    
    const timeUntil = nextAnniversary - now;
    const days = Math.floor(timeUntil / (1000 * 60 * 60 * 24));
    const hours = Math.floor((timeUntil % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    const minutes = Math.floor((timeUntil % (1000 * 60 * 60)) / (1000 * 60));
    const seconds = Math.floor((timeUntil % (1000 * 60)) / 1000);
    
    document.getElementById('days').textContent = days;
    document.getElementById('hours').textContent = hours;
    document.getElementById('minutes').textContent = minutes;
    document.getElementById('seconds').textContent = seconds;
    
    const options = { year: 'numeric', month: 'long', day: 'numeric' };
    document.getElementById('nextDate').textContent = nextAnniversary.toLocaleDateString('en-US', options);
}
