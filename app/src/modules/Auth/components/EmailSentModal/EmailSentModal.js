import { Modal, ModalOverlay, ModalContent, ModalHeader, ModalFooter, ModalBody, ModalCloseButton, Button } from '@chakra-ui/react'

function EmailSentModal({ email, isOpen, onClose }) {


  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Email Sent</ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          Email has been sent to {email} please click the link provided to activate your account
        </ModalBody>
        <ModalFooter>
          <Button onClick={onClose}>Close</Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  )
}

export default EmailSentModal;