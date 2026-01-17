// ShoppingApp - JavaScript Functions

// Initialize AOS (Animate On Scroll)
document.addEventListener('DOMContentLoaded', function() {
    // Initialize AOS
    if (typeof AOS !== 'undefined') {
        AOS.init({
            duration: 800,
            once: true,
            offset: 100
        });
    }

    // Update cart count on page load
    updateCartCount();

    // Add to cart button handlers
    document.querySelectorAll('.add-to-cart-btn').forEach(btn => {
        btn.addEventListener('click', function(e) {
            e.preventDefault();
            const productId = this.dataset.productId;
            addToCart(productId, 1);
        });
    });

    // Navbar scroll effect
    window.addEventListener('scroll', function() {
        const navbar = document.querySelector('.navbar');
        if (window.scrollY > 50) {
            navbar.classList.add('shadow');
        } else {
            navbar.classList.remove('shadow');
        }
    });
});

// Add to Cart function
function addToCart(productId, quantity = 1) {
    const btn = document.querySelector(`[data-product-id="${productId}"]`);
    const originalText = btn.innerHTML;
    
    // Show loading state
    btn.innerHTML = '<span class="spinner-border spinner-border-sm me-1"></span>Adding...';
    btn.disabled = true;

    fetch('/Cart/AddToCart', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
        },
        body: `productId=${productId}&quantity=${quantity}`
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            // Update cart count
            updateCartCountUI(data.cartCount);
            
            // Show toast notification
            showToast(data.message, 'success');
            
            // Reset button
            btn.innerHTML = '<i class="bi bi-check-circle me-1"></i>Added!';
            setTimeout(() => {
                btn.innerHTML = originalText;
                btn.disabled = false;
            }, 2000);
        } else {
            showToast(data.message, 'danger');
            btn.innerHTML = originalText;
            btn.disabled = false;
        }
    })
    .catch(error => {
        console.error('Error:', error);
        showToast('Something went wrong. Please try again.', 'danger');
        btn.innerHTML = originalText;
        btn.disabled = false;
    });
}

// Update cart count
function updateCartCount() {
    fetch('/Cart/GetCartCount')
        .then(response => response.json())
        .then(data => {
            updateCartCountUI(data.count);
        })
        .catch(error => console.error('Error:', error));
}

// Update cart count in UI
function updateCartCountUI(count) {
    const cartBadges = document.querySelectorAll('.cart-count');
    cartBadges.forEach(badge => {
        badge.textContent = count;
        if (count > 0) {
            badge.classList.remove('d-none');
            // Add bounce animation
            badge.classList.add('animate__animated', 'animate__bounce');
            setTimeout(() => {
                badge.classList.remove('animate__animated', 'animate__bounce');
            }, 1000);
        } else {
            badge.classList.add('d-none');
        }
    });
}

// Show toast notification
function showToast(message, type = 'success') {
    const toastEl = document.getElementById('cartToast');
    const toastMessage = document.getElementById('toastMessage');
    
    if (toastEl && toastMessage) {
        // Update toast styling based on type
        toastEl.className = `toast align-items-center text-white bg-${type} border-0`;
        toastMessage.textContent = message;
        
        const toast = new bootstrap.Toast(toastEl, {
            autohide: true,
            delay: 3000
        });
        toast.show();
    }
}

// Format currency
function formatCurrency(amount) {
    return '₹' + new Intl.NumberFormat('en-IN').format(amount);
}

// Debounce function for search
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// Image lazy loading
document.addEventListener('DOMContentLoaded', function() {
    const images = document.querySelectorAll('img[data-src]');
    
    const imageObserver = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const img = entry.target;
                img.src = img.dataset.src;
                img.classList.add('fade-in');
                observer.unobserve(img);
            }
        });
    });

    images.forEach(img => imageObserver.observe(img));
});

// Smooth scroll for anchor links
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function(e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            target.scrollIntoView({
                behavior: 'smooth',
                block: 'start'
            });
        }
    });
});

// Form validation helper
function validateForm(formId) {
    const form = document.getElementById(formId);
    if (!form) return false;
    
    let isValid = true;
    const inputs = form.querySelectorAll('[required]');
    
    inputs.forEach(input => {
        if (!input.value.trim()) {
            input.classList.add('is-invalid');
            isValid = false;
        } else {
            input.classList.remove('is-invalid');
        }
    });
    
    return isValid;
}

// Remove validation error on input
document.querySelectorAll('.form-control, .form-select').forEach(input => {
    input.addEventListener('input', function() {
        this.classList.remove('is-invalid');
    });
});

// Confirmation dialog
function confirmAction(message, callback) {
    if (confirm(message)) {
        callback();
    }
}

// Loading overlay
function showLoading() {
    const overlay = document.createElement('div');
    overlay.id = 'loadingOverlay';
    overlay.className = 'spinner-overlay';
    overlay.innerHTML = `
        <div class="text-center">
            <div class="spinner-border text-primary" style="width: 3rem; height: 3rem;"></div>
            <p class="mt-3 text-muted">Loading...</p>
        </div>
    `;
    document.body.appendChild(overlay);
}

function hideLoading() {
    const overlay = document.getElementById('loadingOverlay');
    if (overlay) {
        overlay.remove();
    }
}

// Number formatting for Indian currency
function formatIndianCurrency(num) {
    const str = num.toString();
    const lastThree = str.substring(str.length - 3);
    const otherNumbers = str.substring(0, str.length - 3);
    if (otherNumbers !== '') {
        return '₹' + otherNumbers.replace(/\B(?=(\d{2})+(?!\d))/g, ',') + ',' + lastThree;
    }
    return '₹' + lastThree;
}
