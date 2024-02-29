/**
 * Add event listener for modal delete so we can pass from a button in a grid
 * a delete action to run specific when we confirm the delete
 * Run immediately
 */
(function () {
		const deleteModal = document.getElementById('modalDelete')
		if (deleteModal) {
				deleteModal.addEventListener('show.bs.modal', event => {

						// Button that triggered the modal
						const button = event.relatedTarget
						// Extract info from data-bs-* attributes
						const deleteAction = button.getAttribute('data-bs-action')
						const objectName = button.getAttribute('data-bs-name')

						// Update the modal's content.
						const modalDeleteButton = document.getElementById('modalConfirmDeleteButton')
						modalDeleteButton.setAttribute('formaction', deleteAction)

						const modalBodyHeader = document.getElementById('modalDeleteObjectName')
						modalBodyHeader.innerText = objectName
				})
		}
}());

