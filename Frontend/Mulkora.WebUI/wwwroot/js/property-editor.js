document.addEventListener("DOMContentLoaded", function () {
    const editor = document.querySelector("[data-property-editor]");

    if (!editor) {
        return;
    }

    const titleInput = editor.querySelector("[data-preview-source='title']");
    const priceInput = editor.querySelector("[data-preview-source='price']");
    const cityInput = editor.querySelector("[data-preview-source='city']");
    const districtInput = editor.querySelector("[data-preview-source='district']");
    const addressInput = editor.querySelector("[data-preview-source='address']");
    const listingTypeInput = editor.querySelector("[data-preview-source='listingType']");
    const categoryInput = editor.querySelector("[data-preview-source='category']");
    const roomInput = editor.querySelector("[data-preview-source='room']");
    const bathroomInput = editor.querySelector("[data-preview-source='bathroom']");
    const grossInput = editor.querySelector("[data-preview-source='gross']");
    const descriptionInput = editor.querySelector("[data-preview-source='description']");
    const imageInput = editor.querySelector("[data-preview-source='images']");
    const featureInputs = editor.querySelectorAll("[data-feature-input]");

    const previewTitle = editor.querySelector("[data-preview-title]");
    const previewPrice = editor.querySelector("[data-preview-price]");
    const previewLocation = editor.querySelector("[data-preview-location]");
    const previewType = editor.querySelector("[data-preview-type]");
    const previewRoom = editor.querySelector("[data-preview-room]");
    const previewBathroom = editor.querySelector("[data-preview-bathroom]");
    const previewGross = editor.querySelector("[data-preview-gross]");
    const previewImage = editor.querySelector("[data-preview-image]");
    const previewFeatureCount = editor.querySelector("[data-preview-feature-count]");

    const hasExistingImage = editor.dataset.hasExistingImage === "true";

    function hasValue(element) {
        return element && element.value && element.value.trim() !== "";
    }

    function updatePreview() {
        if (previewTitle) {
            previewTitle.textContent = hasValue(titleInput) ? titleInput.value : "İlan başlığı";
        }

        if (previewPrice) {
            const price = priceInput ? Number(priceInput.value) : 0;
            previewPrice.textContent = price > 0 ? `₺${new Intl.NumberFormat("tr-TR").format(price)}` : "₺0";
        }

        if (previewLocation) {
            const locationParts = [];

            if (hasValue(districtInput)) {
                locationParts.push(districtInput.value);
            }

            if (hasValue(cityInput)) {
                locationParts.push(cityInput.value);
            }

            previewLocation.textContent = locationParts.length > 0 ? locationParts.join(", ") : "Konum bilgisi";
        }

        if (previewType && listingTypeInput) {
            const selectedOption = listingTypeInput.options[listingTypeInput.selectedIndex];
            previewType.textContent = listingTypeInput.value ? selectedOption.text : "İlan türü";
        }

        if (previewRoom) {
            previewRoom.textContent = hasValue(roomInput) ? roomInput.value : "0";
        }

        if (previewBathroom) {
            previewBathroom.textContent = hasValue(bathroomInput) ? bathroomInput.value : "0";
        }

        if (previewGross) {
            previewGross.textContent = hasValue(grossInput) ? grossInput.value : "0";
        }

        if (previewFeatureCount) {
            const selectedFeatureCount = Array.from(featureInputs).filter(x => x.checked).length;
            previewFeatureCount.textContent = `${selectedFeatureCount} ilan özelliği seçildi`;
        }

        updateProgress();
    }

    function updateProgress() {
        const basicCompleted = hasValue(titleInput) && hasValue(descriptionInput) && Number(priceInput?.value) > 0 && hasValue(listingTypeInput) && hasValue(categoryInput);
        const locationCompleted = hasValue(cityInput) && hasValue(districtInput) && hasValue(addressInput);
        const detailsCompleted = Number(roomInput?.value) > 0 && Number(grossInput?.value) > 0;
        const imagesCompleted = hasExistingImage || (imageInput && imageInput.files && imageInput.files.length > 0);
        const featuresCompleted = Array.from(featureInputs).some(x => x.checked);

        const states = {
            basic: basicCompleted,
            location: locationCompleted,
            details: detailsCompleted,
            images: imagesCompleted,
            features: featuresCompleted
        };

        let completedCount = 0;

        Object.keys(states).forEach(function (key) {
            const item = editor.querySelector(`[data-check='${key}']`);

            if (!item) {
                return;
            }

            item.classList.toggle("complete", states[key]);

            if (states[key]) {
                completedCount++;
            }
        });

        const progressFill = editor.querySelector("[data-progress-fill]");

        if (progressFill) {
            progressFill.style.width = `${completedCount * 20}%`;
        }
    }

    if (imageInput && previewImage) {
        imageInput.addEventListener("change", function () {
            const selectedFile = imageInput.files && imageInput.files.length > 0 ? imageInput.files[0] : null;

            if (selectedFile) {
                previewImage.src = URL.createObjectURL(selectedFile);
            }

            updatePreview();
        });
    }

    editor.addEventListener("input", updatePreview);
    editor.addEventListener("change", updatePreview);

    const navigationLinks = document.querySelectorAll("[data-navigation-link]");
    const sections = document.querySelectorAll("[data-editor-section]");

    if (navigationLinks.length > 0 && sections.length > 0) {
        const observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (!entry.isIntersecting) {
                    return;
                }

                navigationLinks.forEach(x => x.classList.remove("active"));

                const activeLink = document.querySelector(`[data-navigation-link='${entry.target.id}']`);

                if (activeLink) {
                    activeLink.classList.add("active");
                }
            });
        }, {
            rootMargin: "-20% 0px -65% 0px",
            threshold: 0
        });

        sections.forEach(section => observer.observe(section));
    }

    updatePreview();
});