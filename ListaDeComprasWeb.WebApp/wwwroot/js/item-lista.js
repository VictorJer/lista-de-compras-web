(() => {
    const select = document.getElementById("produto-select");
    const category = document.getElementById("product-category");
    const unitPrice = document.getElementById("product-unit-price");
    const swatch = document.getElementById("product-category-color");
    if (!select || !category || !unitPrice || !swatch) return;

    const update = () => {
        const option = select.selectedOptions[0];
        if (!option || !option.value) {
            category.textContent = "Selecione um produto para ver seus detalhes.";
            unitPrice.textContent = "";
            swatch.style.removeProperty("--category-color");
            return;
        }
        category.textContent = option.dataset.category || "Categoria não informada";
        unitPrice.textContent = `${option.dataset.unit || "Unidade"} · ${option.dataset.price || "Preço não informado"}`;
        const color = option.dataset.color || "#71806a";
        swatch.style.setProperty("--category-color", /^#[0-9a-fA-F]{6}$/.test(color) ? color : "#71806a");
    };
    select.addEventListener("change", update);
    update();
})();
