import { useState, useEffect } from "react";
import { Modal, Button, Form } from "react-bootstrap";
import { AppProjectIndividualViewModel } from "./Models/AppProjectIndividualViewModel";
import Datetime from "react-datetime";
import "react-datetime/css/react-datetime.css";
import moment from "moment";
import axios from "axios";

interface EditProjectModalProps {
    show: boolean;
    onHide: () => void;
    project: AppProjectIndividualViewModel | null;
}

const EditProjectModal = ({ show, onHide, project }: EditProjectModalProps) => {
    const [formData, setFormData] = useState<AppProjectIndividualViewModel | null>(null);
    const isEditMode = project !== null;

    useEffect(() => {
        if (isEditMode && project) {
            setFormData({
                ...project,
                openDt: project.openDt ? moment(project.openDt).toISOString() : "",
                startDt: project.startDt ? moment(project.startDt).toISOString() : "",
                completedDt: project.completedDt ? moment(project.completedDt).toISOString() : "",
                id: project.id
            });
        } else {
            setFormData({
                projectName: "",
                projectRef: "",
                projectLocation: "",
                openDt: "",
                startDt: "",
                completedDt: "",
                projectValue: 0,
                statusLevel: "",
                notes: ""
            });
        }
    }, [project]);

    const handleSave = async () => {
        if (formData) {
            // Remove $id field
            const { $id, ...cleanFormData } = formData;

            // Ensure notes is not null
            cleanFormData.notes = cleanFormData.notes || "";

            try {
                if (isEditMode && cleanFormData.id) {
                    console.log("Editing ");
                    await axios.put(`/api/appproject/${cleanFormData.id}`, cleanFormData);
                } else {
                    await axios.post("/api/appproject", cleanFormData);
                }
                onHide();
            } catch (error) {
                console.error("Error saving the project:", error);
            }
        }
    };

    const handleDateChange = (name: string, date: any) => {
        setFormData({
            ...formData!,
            [name]: date ? date.toISOString() : ""
        });
    };

    return (
        <Modal show={show} onHide={onHide}>
            <Modal.Header closeButton>
                <Modal.Title>{isEditMode ? "Edit Project" : "Create Project"}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {formData && (
                    <Form>
                        <Form.Group controlId="formProjectName">
                            <Form.Label>Project Name</Form.Label>
                            <Form.Control
                                type="text"
                                value={formData.projectName || ""}
                                onChange={(e) => setFormData({ ...formData, projectName: e.target.value })}
                            />
                        </Form.Group>

                        <Form.Group controlId="formProjectRef">
                            <Form.Label>Project Reference</Form.Label>
                            <Form.Control
                                type="text"
                                value={formData.projectRef || ""}
                                onChange={(e) => setFormData({ ...formData, projectRef: e.target.value })}
                            />
                        </Form.Group>

                        <Form.Group controlId="formProjectLocation">
                            <Form.Label>Project Location</Form.Label>
                            <Form.Control
                                type="text"
                                value={formData.projectLocation || ""}
                                onChange={(e) => setFormData({ ...formData, projectLocation: e.target.value })}
                            />
                        </Form.Group>

                        <Form.Group controlId="formOpenDt">
                            <Form.Label>Open Date</Form.Label>
                            <Datetime
                                value={formData.openDt || ""}
                                onChange={(date) => handleDateChange("openDt", date)}
                                dateFormat="YYYY-MM-DD"
                                timeFormat="HH:mm"
                                inputProps={{ className: "form-control" }}
                            />
                        </Form.Group>

                        <Form.Group controlId="formStartDt">
                            <Form.Label>Start Date</Form.Label>
                            <Datetime
                                value={formData.startDt || ""}
                                onChange={(date) => handleDateChange("startDt", date)}
                                dateFormat="YYYY-MM-DD"
                                timeFormat="HH:mm"
                                inputProps={{ className: "form-control" }}
                            />
                        </Form.Group>

                        <Form.Group controlId="formCompletedDt">
                            <Form.Label>Completed Date</Form.Label>
                            <Datetime
                                value={formData.completedDt || ""}
                                onChange={(date) => handleDateChange("completedDt", date)}
                                dateFormat="YYYY-MM-DD"
                                timeFormat="HH:mm"
                                inputProps={{ className: "form-control" }}
                            />
                        </Form.Group>

                        <Form.Group controlId="formProjectValue">
                            <Form.Label>Project Value</Form.Label>
                            <Form.Control
                                type="number"
                                value={formData.projectValue || 0}
                                onChange={(e) => setFormData({ ...formData, projectValue: parseFloat(e.target.value) })}
                            />
                        </Form.Group>

                        <Form.Group controlId="formStatusLevel">
                            <Form.Label>Status Level</Form.Label>
                            <Form.Control
                                type="text"
                                value={formData.statusLevel || ""}
                                onChange={(e) => setFormData({ ...formData, statusLevel: e.target.value })}
                            />
                        </Form.Group>

                        <Form.Group controlId="formNotes">
                            <Form.Label>Notes</Form.Label>
                            <Form.Control
                                as="textarea"
                                rows={3}
                                value={formData.notes || ""}
                                onChange={(e) => setFormData({ ...formData, notes: e.target.value })}
                            />
                        </Form.Group>
                    </Form>
                )}
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={onHide}>
                    Close
                </Button>
                <Button variant="primary" onClick={handleSave}>
                    {isEditMode ? "Save Changes" : "Create Project"}
                </Button>
            </Modal.Footer>
        </Modal>
    );
};

export default EditProjectModal;
