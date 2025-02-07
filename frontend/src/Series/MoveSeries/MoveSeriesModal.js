import PropTypes from 'prop-types';
import React from 'react';
import Button from 'Components/Link/Button';
import Modal from 'Components/Modal/Modal';
import ModalBody from 'Components/Modal/ModalBody';
import ModalContent from 'Components/Modal/ModalContent';
import ModalFooter from 'Components/Modal/ModalFooter';
import ModalHeader from 'Components/Modal/ModalHeader';
import { kinds, sizes } from 'Helpers/Props';
import styles from './MoveSeriesModal.css';

function MoveSeriesModal(props) {
  const {
    originalPath,
    destinationPath,
    destinationRootFolder,
    isOpen,
    onModalClose,
    onSavePress,
    onMoveSeriesPress
  } = props;

  if (
    isOpen &&
    !originalPath &&
    !destinationPath &&
    !destinationRootFolder
  ) {
    console.error('orginalPath and destinationPath OR destinationRootFolder must be provided');
  }

  return (
    <Modal
      isOpen={isOpen}
      size={sizes.MEDIUM}
      closeOnBackgroundClick={false}
      onModalClose={onModalClose}
    >
      <ModalContent
        showCloseButton={true}
        onModalClose={onModalClose}
      >
        <ModalHeader>
          Move Files
        </ModalHeader>

        <ModalBody>
          {
            destinationRootFolder ?
              `Would you like to move the sites folders to '${destinationRootFolder}'?` :
              `Would you like to move the sites files from '${originalPath}' to '${destinationPath}'?`
          }
        </ModalBody>

        <ModalFooter>
          <Button
            className={styles.doNotMoveButton}
            onPress={onSavePress}
          >
            No, I'll Move the Files Myself
          </Button>

          <Button
            kind={kinds.DANGER}
            onPress={onMoveSeriesPress}
          >
            Yes, Move the Files
          </Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
}

MoveSeriesModal.propTypes = {
  originalPath: PropTypes.string,
  destinationPath: PropTypes.string,
  destinationRootFolder: PropTypes.string,
  isOpen: PropTypes.bool.isRequired,
  onModalClose: PropTypes.func.isRequired,
  onSavePress: PropTypes.func.isRequired,
  onMoveSeriesPress: PropTypes.func.isRequired
};

export default MoveSeriesModal;
