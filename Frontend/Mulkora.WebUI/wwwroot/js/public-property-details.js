document.addEventListener("DOMContentLoaded", function () {
    const lightbox = document.querySelector("[data-lightbox]");
    const lightboxImage = document.querySelector("[data-lightbox-image]");
    const lightboxClose = document.querySelector("[data-lightbox-close]");
    const galleryImages = document.querySelectorAll("[data-gallery-image]");
    const shareButton = document.querySelector("[data-share-button]");
    const shareToast = document.querySelector("[data-share-toast]");

    function openLightbox(imageUrl) {
        if (!lightbox || !lightboxImage) {
            return;
        }

        lightboxImage.src = imageUrl;
        lightbox.hidden = false;
        document.body.style.overflow = "hidden";
    }

    function closeLightbox() {
        if (!lightbox || !lightboxImage) {
            return;
        }

        lightbox.hidden = true;
        lightboxImage.src = "";
        document.body.style.overflow = "";
    }

    galleryImages.forEach(function (button) {
        button.addEventListener("click", function () {
            openLightbox(button.dataset.galleryImage);
        });
    });

    if (lightboxClose) {
        lightboxClose.addEventListener("click", closeLightbox);
    }

    if (lightbox) {
        lightbox.addEventListener("click", function (event) {
            if (event.target === lightbox) {
                closeLightbox();
            }
        });
    }

    document.addEventListener("keydown", function (event) {
        if (event.key === "Escape" && lightbox && !lightbox.hidden) {
            closeLightbox();
        }
    });

    if (shareButton) {
        shareButton.addEventListener("click", async function () {
            const shareData = {
                title: shareButton.dataset.shareTitle,
                url: window.location.href
            };

            if (navigator.share) {
                try {
                    await navigator.share(shareData);
                    return;
                } catch {
                    return;
                }
            }

            try {
                await navigator.clipboard.writeText(window.location.href);

                if (shareToast) {
                    shareToast.hidden = false;

                    window.setTimeout(function () {
                        shareToast.hidden = true;
                    }, 2500);
                }
            } catch {
                window.prompt("İlan bağlantısını kopyalayın:", window.location.href);
            }
        });
    }
});