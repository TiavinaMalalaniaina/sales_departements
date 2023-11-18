import {
    CButton,
    CCard,
  CCardBody,
  CCardSubtitle,
  CCardTitle,
  CCol,
  CRow
} from '@coreui/react'
import { useState } from "react"
import RequestProductValidationTable from '../tables/RequestProductValidationTable'



const RequestProductValidation = () => {
    const [data, setData] = useState(
        [
            {
                productId: 1,
                productName: 'P1',
                quantity: 500,
                motif: 'Je le veut, c\'est tout',
            },
            {
                productId: 1,
                productName: 'P1',
                quantity: 500,
                motif: 'Je le veut, c\'est tout',
            },
            {
                productId: 1,
                productName: 'P1',
                quantity: 500,
                motif: 'Je le veut, c\'est tout',
            },
            {
                productId: 1,
                productName: 'P1',
                quantity: 500,
                motif: 'Je le veut, c\'est tout',
            }
        ]
    )

    return (
        <>
            <CRow>
                <CCol xs={12}>
                    <CCard className="mb-4">
                        <CCardBody>
                            <CCardTitle>Demande du <strong>2022-01-01</strong></CCardTitle>
                            <CCardSubtitle>Par <strong>Tiavina</strong></CCardSubtitle>
                            <CButton className='text-end'>Validé</CButton>
                            <RequestProductValidationTable data={data}/>
                        </CCardBody>
                    </CCard>
                </CCol>
            </CRow>
        </>
    )
}
export default RequestProductValidation
